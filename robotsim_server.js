const https = require('https'),
    fs = require('fs'),
    child_process = require('child_process');

const robotmap = new Map();

const removeNewlines = (str) =>
    str.replace(/\n/g, '');

const startRobot = (n, name) => {
    if (robotmap.has(n)) {
        return [false, 'robot already running on port\n'];
    }
    // TODO give port to robot
    try {
        const rob = child_process.spawn('pipenv', ['run', 'python', '/robotsim/main.py'], { env: { ...process.env, 'PIPENV_PIPFILE': '/robotsim/Pipfile' } });
        robotmap.set(n, [rob, name]);
        rob.stdin.write(`${removeNewlines(name)}\n${n}\n`);
        //rob.stderr.on('data', d => console.log(d.toString('utf8')));
        //rob.stdout.on('data', d => console.log(d.toString('utf8')));
        return [true, `robot ${n} created\n`];
    } catch (e) {
        return [false, String(e).concat('\n')];
    }
}

// TODO: use name
const addHandler = (n, name, res) => {
    if (n <= 10000 || n >= 20000) {
        res.writeHead(400);
        res.end(`${n} not between 10000 and 20000`);
        return;
    }
    const [success, msg] = startRobot(n, name);
    if (!success) {
        res.writeHead(400);
        res.end(msg);
        return;
    }
    res.writeHead(202);
    res.end(msg);
};

const stopHandler = (n, res) => {
    const robot = robotmap.get(n);
    if (typeof robot === 'undefined') {
        res.writeHead(404);
        res.end(`Robot ${n} not found`);
        return;
    }
    robot[0].kill('SIGINT');
    robotmap.delete(n);
    res.writeHead(202);
    res.end(`Stopped robot ${n}\n`);
};

const robotDiv = (port, name) => `createRobotdiv(${port}, "${name.replace(/["\\]/g, char => char === '"' ? '\\"' : '\\\\')}")`;

const indexHandler = (res) => {
    res.writeHead(200, { 'Content-Type': 'text/html' });
    res.end(`<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>ROBOT MANAGER</title>
</head>

<body>
    <div id=inputdiv>
        <form id="create-robot">
            <label for="port-i">Choose port from 10001 to 19999</label><input type="number" min="10001" max="19999"
                value="10001" id="port-i"></input><br />
            <label for="name-i">Choose name for the robot</label><input type="text" value="unnamed" minlength="1"
                id="name-i"></input><br />
            <input type="button" value="ADD ROBOT" id="add-robot-i"></input>
        </form>
    </div>
    <script>
        const showErr = (err) => {
            console.warn(err);
            const errp = document.createElement('p');
            errp.style.setProperty('color', '#FF0000');
            errp.innerText = String(err);
            document.getElementById('inputdiv').appendChild(errp);
        };

        const createRobotdiv = (port, name) => {
            const robotdiv = document.createElement('div');
            robotdiv.innerText = '[PORT: '.concat(port, ', NAME: "', name, '"]');
            robotdiv.id = port;
            const removeRobotdiv = document.createElement('button');
            removeRobotdiv.onclick = (e) => {
                fetch('/stop/'.concat(port)).then(async res => {
                    if (res.status === 202 || res.status === 404) {
                        robotdiv.parentElement.removeChild(robotdiv);
                    } else {
                        const restxt = await res.text();
                        showErr(restxt);
                    }
                }, reason => showErr(reason));
            };
            removeRobotdiv.innerText = 'STOP';
            robotdiv.appendChild(removeRobotdiv);
            document.body.appendChild(robotdiv);
        };

        const addRobot = (evt) => {
            evt.preventDefault();
            const port = document.getElementById('port-i').value,
                name = document.getElementById('name-i').value;
            if (name.length < 1) {
                showErr('Name must be at least 1 character long');
                return false;
            }
            fetch('/start/'.concat(port, '/', encodeURIComponent(name))).then(
                async res => {
                    if (res.status === 202) {
                        createRobotdiv(port, name);
                    } else {
                        const restxt = await res.text();
                        showErr(restxt);
                    }
                },
                reason =>
                    showErr(reason)
            );
        };

        document.getElementById('add-robot-i').onclick = addRobot;
    </script>
    <script>
        ${[...robotmap].map(([port, [{ }, name]]) => robotDiv(port, name)).join('\n        ')}
    </script>
</body>

</html>`);
};

const requestHandler = ({ url }, res) => {
    let match;
    if ((match = url.match(/^\/start\/(\d+)\/((?:[\w-\.~!\*'\(\)]|(?:%[\dA-F]{2}))+)$/)) !== null) {
        addHandler(parseInt(match[1]), decodeURIComponent(match[2]), res);
    } else if ((match = url.match(/^\/stop\/(\d+)$/)) !== null) {
        stopHandler(parseInt(match[1]), res);
    } else if ((match = url.match(/^\/$/)) !== null) {
        indexHandler(res);
    } else {
        res.writeHead(404);
        res.end('404\n');
    }
};

https.createServer(
    {
        key: fs.readFileSync('key.pem'),
        cert: fs.readFileSync('cert.pem')
    },
    requestHandler
).listen(10000);
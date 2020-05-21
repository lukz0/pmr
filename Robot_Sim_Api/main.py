from models.users import *
from models.status import *
from models.registers import *
from models.mission import *
from models.mission_queue import *
from models.statistics import *


mainvars = {
    "robotname": "",
    "port": 0
}

if __name__ == '__main__':
    mainvars["robotname"] = input("Enter robot name: ")
    while True:
        try:
            mainvars["port"] = int(input("Enter port number"))
            break
        except ValueError:
            print("Port must be an integer")
    # debug changed to False to avoid server restarts while being used by a script
    # you can temporary change to true while developing to restart server automatically on code change
    app.run(debug=False, port=mainvars["port"], host="0.0.0.0")

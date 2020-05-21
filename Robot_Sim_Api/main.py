from models.users import *
from models.status import *
from models.status import Status
from models.registers import *
from models.mission import *
from models.mission_queue import *
from models.statistics import *

port = 0
while True:
    try:
        set_robot_name(input("Enter robot name: "))
        port = int(input("Enter port number"))
        break
    except ValueError:
        print("Port must be an integer")


if __name__ == '__main__':
    # debug changed to False to avoid server restarts while being used by a script
    # you can temporary change to true while developing to restart server automatically on code change
    app.run(debug=False, port=port, host="0.0.0.0")

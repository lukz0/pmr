from models.users import *
from models.status import *
from models.registers import *
from models.mission import *
from models.mission_queue import *
from models.statistics import *


class Mainvars:
    def __init__(self):
        if "mainvars" not in globals():
            self.robotname = input("Enter robot name: ")
            while True:
                try:
                    self.port = int(input("Enter port number: "))
                    break
                except ValueError:
                    print("Port must be an integer")
        else:
            self.robotname = globals().get("mainvars").robotname
            self.port = globals().get("mainvars").port


mainvars = Mainvars()

if __name__ == '__main__':
    # debug changed to False to avoid server restarts while being used by a script
    # you can temporary change to true while developing to restart server automatically on code change
    app.run(debug=False, port=mainvars.port, host="0.0.0.0")

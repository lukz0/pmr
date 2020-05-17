from models.users import *
from models.status import *
from models.registers import *
from models.mission import *
from models.mission_queue import *

port = 0
while True:
    try:
        port = int(input("enter port number"))
        break
    except ValueError:
        print("port must be an integer")

if __name__ == '__main__':
    # debug changed to False to avoid server restarts while being used by a script
    # you can temporary change to true while developing to restart server automatically on code change
    app.run(debug=False, port=port)

from models.status import *
from models.registers import *
from models.mission import *
from models.mission_queue import *

if __name__ == '__main__':
    app.run(debug=True, port='5003')

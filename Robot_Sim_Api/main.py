from models.users import *
from models.status import *
from models.registers import *
from models.mission import *
from models.mission_queue import *



if __name__ == '__main__':
    # Test - TODO print the info in termical
    host_name = 'MIR_2X23'
    app.run(debug=True, port='5003')

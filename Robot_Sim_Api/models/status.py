from app import *
import datetime
from models.mission_queue import mission_queue_id, missionQueueDAO
from models.statistics import distance_list
from main import mainvars

"""
General status information about the robot including battery voltage, state, uptime, total run distance, current job and map.
"""

error_model = api.model('errors', {
    'code': fields.Float,
    'description': fields.String,
    'module': fields.String
})

cart_model = api.model('cart', {
    'height': fields.Float,
    'id': fields.Float,
    'length': fields.Float,
    'offset_locked_wheels': fields.Float,
    'width': fields.Float
})

hook_status = api.model('hook_status', {
    'angle': fields.Float,
    'available': fields.Boolean,
    'braked': fields.Boolean,
    'cart': fields.Nested(cart_model, allow_null=True),
    'cart_attached': fields.Boolean,
    'height': fields.Float,
    'length': fields.Float
})

position_model = api.model('position', {
    'orientation': fields.Float,
    'x': fields.Float,
    'y': fields.Float
})

user_prompt_model = api.model('user_prompt', {
    'guid': fields.String,
    'options': fields.List(fields.String),
    'question': fields.String,
    'timeout': fields.Float,
    'user_group': fields.String
})

velocity_model = api.model('velocity', {
    'angular': fields.Integer,
    'linear': fields.Float
})

#---------------------------------------------------------

status_model = api.model('status', {
    'battery_percentage': fields.Float(required=False, description='The current charge percentage of the battery'),
    'battery_time_remaining': fields.Integer(required=False),
    'distance_to_next_target': fields.Float(required=False),
    'errors': fields.List(fields.Nested(error_model),
                          description='The list of actions executed as part of the mission'),
    'footprint': fields.String,
    'map_id': fields.String,
    'mission_queue_id': fields.Integer(required=False),
    'mission_queue_url': fields.String(required=False),
    'mission_text': fields.String,
    'mode_id': fields.Integer,
    'mode_text': fields.String,
    'moved': fields.Float,
    'position': fields.Nested(position_model),
    'robot_model': fields.String,
    'robot_name': fields.String,
    'serial_number': fields.String,
    'session_id': fields.String,
    'state_id': fields.Integer,
    'state_text': fields.String,
    'unloaded_map_changes': fields.Boolean,
    'uptime': fields.Integer,
    'user_prompt': fields.Nested(user_prompt_model),
    'velocity': fields.Nested(velocity_model)
})

starttime = datetime.datetime.now()
serial = None


def get_uptime():
    return int((datetime.datetime.now()-starttime).total_seconds())


def get_serial():
    global serial
    if serial is not None:
        return serial
    port_str = str(mainvars["port"])
    serial_str = "180200011100697"
    try:
        serial = ''.join([serial_str[:len(serial_str)-len(port_str)], port_str])
    except IndexError:
        serial = port_str[:len(serial_str)]
    return serial


def generate_status():
    battery_percent = (lambda t: (t.microsecond/1000 + t.second*1000 + t.minute*60000)/3.6e6 * 100)(datetime.datetime.now())
    battery_time = 6685*battery_percent
    return {
        "battery_precentage": battery_percent,
        "battery_time_remaining": battery_time,
        "distance_to_next_target": 0.0,
        "errors": {},
        "footprint": "[[0.506,-0.32],[0.506,0.32],[-0.454,0.32],[-0.454,-0.32]]",
        "map_id": "e11fc4eb-b819-11e9-b021-94c69118fd1e",
        "mission_queue_id": mission_queue_id,
        "mission_queue_url": "/v2.0.0/mission_queue/{}".format(mission_queue_id),
        "mission_text": missionQueueDAO.get(mission_queue_id).state if mission_queue_id != 0 else "",
        "mode_id": 7,
        "mode_text": "Mission",
        "moved": distance_list[-1].distance,
        "position": {
            "orientation": battery_percent * 3.6,
            "x": battery_percent,
            "y": 100-battery_percent
        },
        "robot_model": "MiR200",
        "robot_name": mainvars["robotname"],
        "serial_number": get_serial(),
        "session_id": "bcf29362-4f7d-11e8-a97a-94c69118fd1e",
        "state_id": 3,
        "state_text": "Ready",
        "unloaded_map_changes": False,
        "uptime": get_uptime(),
        "user_prompt": {
            "guid": "",
            "options": [],
            "question": "",
            "timeout": 0,
            "user_group": ""
        },
        "velocity": {
            "angular": battery_percent/100,
            "linear": 1-(battery_percent/100)
        }
    }


# -----------------------------------------------------------
# GET status /
# -----------------------------------------------------------

@api.doc(responses={400: 'Invalid ordering or Invalid filters or Wrong output fields or Invalid limits'})
@api.doc(responses={404: 'Not found'})
@api.doc(responses={202: 'Successfully retrieve the specified element'})
@api.route('/api/v2.0.0/status')
class Status(Resource):
    @auth_required
    @api.marshal_with(status_model)
    def get(self):
        return generate_status()

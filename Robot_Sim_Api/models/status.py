from app import *
from flask_restplus import Resource, fields

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
    'options': fields.String,  # array av string? < string > array
    'question': fields.String,
    'timeout': fields.Float,
    'user_group': fields.String
})

velocity_model = api.model('velocity', {
    'angular': fields.Integer,
    'linear': fields.Float
})

status_model = api.model('status', {
    'battery_percentage': fields.Float(required=False, description='The current charge percentage of the battery'),
    'battery_time_remaining': fields.Integer(required=False),
    'distance_to_next_target': fields.Float(required=False),
    'errors': fields.List(fields.Nested(error_model),
                          description='The list of actions executed as part of the mission'),
    'footprint': fields.String,
    'hook_status': fields.Nested(error_model),
    'map_id': fields.String,
    'mission_queue_id': fields.Integer,
    'mission_queue_url': fields.String,
    'mission_text': fields.String,
    'mode_id': fields.Integer,
    'mode_text': fields.String,
    'moved': fields.Float,
    'position': fields.Nested(position_model),
    'robot_model': fields.String,
    'robot_name': fields.String,
    'serial_number': fields.String,
    'state_id': fields.Integer,
    'state_text': fields.String,
    'unloaded_mop_changes': fields.Boolean,
    'uptime': fields.Integer,
    'user_prompt': fields.Nested(user_prompt_model),
    'velocity': fields.Nested(velocity_model)
})

status = [{
    "allowed_methods": 'null',
    "battery_percentage": 94.4000015258789,
    "battery_time_remaining": 45305,
    "distance_to_next_target": 0,
    "errors": [],
    "footprint": "[[0.506,-0.32],[0.506,0.32],[-0.454,0.32],[-0.454,-0.32]]",
    "map_id": "e11fc4eb-b819-11e9-b021-94c69118fd1e",
    "mission_queue_id": 'null',
    "mission_queue_url": 'null',
    "mission_text": "Starting...",
    "mode_id": 7,
    "mode_text": "Mission",
    "moved": 143470.47,
    "position": {
        "orientation": 87.9072265625,
        "x": 32.924983978271484,
        "y": 37.166587829589844
    },
    "robot_model": "MiR200",
    "robot_name": "MiR_S274",
    "serial_number": "180200011100274",
    "session_id": "bcf29362-4f7d-11e8-a97a-94c69118fd1e",
    "state_id": 4,
    "state_text": "Pause",
    "unloaded_map_changes": 'false',
    "uptime": 2925,
    "user_prompt": 'null',
    "velocity": {
        "angular": 0,
        "linear": 0
    }
}]


# -----------------------------------------------------------
# GET status /
# -----------------------------------------------------------

@api.doc(responses={400: 'Invalid ordering or Invalid filters or Wrong output fields or Invalid limits'})
@api.doc(responses={404: 'Not found'})
@api.doc(responses={202: 'Successfully retrieve the specified element'})
@api.route('/status')
class Status(Resource):
    #@api.marshal_with(status_model)
    def get(self):
        return status

    @api.expect(status_model)
    def post(self):
        status.append(api.payload)
        return {"success": "true", "description": "Successfully added mission to mission queue", "id": "320"}, 201

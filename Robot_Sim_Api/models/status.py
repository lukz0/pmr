from app import *

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

status_robot_1 = {
    'battery_percentage': 59.56,
    'battery_time_remaining': 46505,
    'distance_to_next_target': 0.0,
    'errors': {}, # se på envelop
    'footprint': "[[0.506,-0.32],[0.506,0.32],[-0.454,0.32],[-0.454,-0.32]]",
    'map_id': "e11fc4eb-b819-11e9-b021-94c69118fd1e",
    'mission_queue_id': 0,
    'mission_queue_url': 'null',
    'mission_text': "Starting...",
    'mode_id': 7,
    'mode_text': "Mission",
    'moved': 143470.47,
    'position': {
        "orientation": 87.9072265625,
        "x": 32.924983978271484,
        "y": 37.166587829589844
    },
    'robot_model': "MiR200",
    'robot_name': "MiR_S274",
    'serial_number': "180200011100274",
    'session_id': "bcf29362-4f7d-11e8-a97a-94c69118fd1e",
    'state_id': 4,
    'state_text': "Pause",
    'unloaded_map_changes': False,
    'uptime': 1830,
    'user_prompt': {
        'guid': '.',
        'options': 'null',  # array av string? < string > array
        'question': 'null',
        'timeout': 0,
        'user_group': 'null'
    },
    'velocity': {
        "angular": 0.0,
        "linear": 0.0
    }
}

status_robot_2 = {
    'battery_percentage': 69.56,
    'battery_time_remaining': 4650,
    'distance_to_next_target': 0.0,
    'errors': {}, # se på envelop
    'footprint': "[[0.506,-0.32],[0.506,0.32],[-0.454,0.32],[-0.454,-0.32]]",
    'map_id': "e11fc4eb-b819-11e9-b021-94c69118fd1e",
    'mission_queue_id': 2,
    'mission_queue_url': '/v2.0.0/mission_queue/2',
    'mission_text': "Starting...",
    'mode_id': 7,
    'mode_text': "Mission",
    'moved': 111470.97,
    'position': {
        "orientation": 36.90265625,
        "x": 11.924983978271484,
        "y": 16.166587829589844
    },
    'robot_model': "MiR200",
    'robot_name': "MiR_S365",
    'serial_number': "180200011100697",
    'session_id': "lpo29682-4f7d-11e8-a97b-94c69118fd1e",
    'state_id': 3,
    'state_text': "Ready",
    'unloaded_map_changes': False,
    'uptime': 1960,
    'user_prompt': {
        'guid': 'null',
        'options': 'null',  # array av string? < string > array
        'question': 'null',
        'timeout': 0,
        'user_group': 'null'
    },
    'velocity': {
        "angular": 0.0,
        "linear": 0.0
    }
}

status = [status_robot_1, status_robot_2]


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
        return status_robot_1

    @auth_required
    @api.expect(status_model)
    def post(self):
        status.append(api.payload)
        return {"success": "true", "description": "Successfully added mission to mission queue", "id": "320"}, 201

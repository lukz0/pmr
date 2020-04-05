from flask import Flask
from flask_restplus import Api, Resource, fields

app = Flask(__name__)
api = Api(app)

# -----------------------------------------------------------
# GET status /on robot
# -----------------------------------------------------------
status_map_model = api.model('map', {'id': fields.String,
                                     'name': fields.String

                                     })

status_model = api.model('status',
                         {'battery': fields.Float,
                          'state': fields.String,
                          "uptime": fields.Integer,
                          'distance': fields.Float,
                          'job': fields.String,
                          'map': fields.Nested(status_map_model)
                          })

status_one = {'battery': 28.51,
              'state': 'Pause',
              "uptime": 3160,
              'distance': 212.38,
              'job': '1079',
              'map': {'name': 'ConfigurationMap',
                      'id': '1'
                      }}
status = [status_one]


@api.route('/json/status')
class Status(Resource):
    @api.marshal_with(status_model)#this allows the model to be visible in
    def get(self):                 #in the swagger page
        return status


# -----------------------------------------------------------
# GET robot status
# -----------------------------------------------------------
pos_model = api.model('position', {'x': fields.String,
                                   'y': fields.String,
                                   'theta': fields.String
                                   })

robot_model = api.model('robot',
                        {'state': fields.String,
                         "distance_to_target": fields.String,
                         'battery_time_left_seconds': fields.String,
                         'position': fields.Nested(pos_model)
                         })

robot_one = {'state': 'InTransit',
             "distance_to_target": '8.3',
             'battery_time_left_seconds': '52918',
             'position': {
                 'x': '3.69',
                 'y': '-5.62',
                 'theta': '148.07'
             }}

robots = [robot_one]


@api.route('/json/robot')
class Robot_Status(Resource):
    @api.marshal_with(robot_model)
    def get(self):
        return robots

    # def POST(payload):


# -----------------------------------------------------------
# GET active missions
# -----------------------------------------------------------
active_mission_model = api.model('active_missions',
                                 {"success": fields.String,
                                  "description": fields.String,
                                  "id": fields.String,
                                  "name": fields.String,
                                  "state": fields.String,
                                  })

active_mission_one = {"success": "true",
                      "description": 'Active mission found',
                      "id": '313',
                      "name": 'Test',
                      "state": 'Executing',
                      }
active_missions = [active_mission_one]


@api.route('/json/mission/active')
class Active_Missions(Resource):
    @api.marshal_with(active_mission_model)
    def get(self):
        return active_missions


# -----------------------------------------------------------
# GET available missions
# -----------------------------------------------------------
mission_items_model1 = api.model('items',
                                 {'name1': fields.String,
                                  'name2': fields.String
                                  })

available_mission_model = api.model('available_missions',
                                    {'success': fields.String,
                                     'size': fields.Integer,
                                     "items": fields.Nested(mission_items_model1)
                                     })

available_mission_one = {'success': 'true',
                         'size': 2,
                         "items": {
                             'name1': 'TestMission',
                             'name2': 'DockingMission'}
                         }
available_missions = [available_mission_one]


@api.route('/json/mission/available')
class Available_Mis(Resource):
    @api.marshal_with(available_mission_model)
    def get(self):
        return available_missions


# -----------------------------------------------------------
# GET positions missions
# -----------------------------------------------------------
mission_items_model2 = api.model('items',
                                 {'name1': fields.String,
                                  'name2': fields.String,
                                  'name3': fields.String
                                  })

positions_mission_model = api.model('positions_missions',
                                    {'success': fields.String,
                                     'size': fields.Integer,
                                     "items": fields.Nested(mission_items_model2)
                                     })

positions_mission_one = {'success': 'true',
                         'size': 3,
                         "items": {
                             'name1': 'Config position',
                             'name2': 'Workshop',
                             'name3': 'Port'}
                         }
positions_missions = [positions_mission_one]


@api.route('/json/mission/positions')
class Positions_Mis(Resource):
    @api.marshal_with(positions_mission_model)
    def get(self):
        return positions_missions


# -----------------------------------------------------------
# GET queue missions
# -----------------------------------------------------------
queue_item_model1 = api.model('item',
                              {'id': fields.String,
                               'name': fields.String,
                               'state': fields.String
                               })
queue_item_model2 = api.model('items',
                              {'item1': fields.Nested(queue_item_model1),
                               'item2': fields.Nested(queue_item_model1),
                               })

queue_mission_model = api.model('queue_missions',
                                {'success': fields.String,
                                 'size': fields.Integer,
                                 "items": fields.Nested(queue_item_model2)
                                 })
queue_items_one = {'item1': {'id': '314',
                             'name': 'DockingMission',
                             'state': 'Abort'},
                   'item2': {'id': '315',
                             'name': 'M_TAXI_TO;Config position',
                             'state': 'Pending'}
                   }

queue_mission_one = {'success': 'true',
                     'size': 2,
                     "items": queue_items_one
                     }
queue_missions = [queue_mission_one]


@api.route('/json/mission/queue')
class Queue_Mis(Resource):
    @api.marshal_with(queue_mission_model)
    def get(self):
        return queue_missions


# -----------------------------------------------------------
# POST missions to queue
# -----------------------------------------------------------
post_mission_model = api.model('mission',
                               {'type': fields.String,
                                'name': fields.String('the mission name')
                                })
mission_queue = []


@api.route('/json/mission')
class Post_Mis(Resource):

    @api.marshal_with(post_mission_model)
    def get(self):
        return mission_queue

    @api.expect(post_mission_model)
    def post(self):
        mission_queue.append(api.payload)
        return {'{"success":"true","description":"Successfully added mission to mission queue","id":"320"}'}, 201


if __name__ == '__main__':
    app.run(debug=True)

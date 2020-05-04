'''from models.mission import *

getMissionQueues_model = api.model('GetMissionQueue', {
    'id': fields.Integer,
    'state': fields.String,
    'url': fields.String
})


class MissionQueueItem:
    def __init__(self, i, state, url):
        self.id = i
        self.state = state
        self.url = url

    def __str__(self):
        return str(self.__class__) + ': ' + str(self.__dict__)


# TODO: missions change state and have duration
class MissionQueueDAOClass:
    def __init__(self):
        self.queue = []
        self.currentID = 0

    def add(self, state, url):
        self.currentID += 1
        self.queue.append(MissionQueueItem(self.currentID, state, url))

    def get(self, i):
        if i in self.queue:
            return self.queue[i]
        else:
            api.abort(404, "Mission with id {} doesn't exist".format(i))

    def __str__(self):
        return str(self.__class__) + ': ' + str(self.__dict__)


MissionQueueDAO = MissionQueueDAOClass()


@api.route('/json/v2.0.0/mission_queue')
class MissionQueue(Resource):
    @ns.marshal_with(getMissionQueues_model)
    def get(self):
        return MissionQueueDAO.queue, 200


@api.route('/json/v2.0.0/mission_queue/<int:i>')
@ns.param('i', 'Mission id')
class MissionQueueID(Resource):
    @ns.marshal_with(getMissionQueues_model)
    def get(self, i):
        return MissionQueueDAO.get(i), 200'''

from models.mission import *
from heapq import heappop, heappush

getMissionQueues_model = api.model('GetMissionQueue', {
    'id': fields.Integer,
    'state': fields.String,
    'url': fields.String
})

postMissionQueues_model = api.model('PostMissionQueue', {
    'fleet_schedule_guid': fields.String(max_length=36),
    'message': fields.String(max_length=200),
    'mission_id': fields.String(min_length=1), # REQUIRED
    'parameters': fields.List(fields.Wildcard),
    'priority': fields.Float
})

getMissionQueuesSpecific_model = api.model('GetMissionQueueSpecific', {
    'actions': fields.String,
    'control_posid': fields.String,
    'control_state': fields.Integer,
    'created_by': fields.String,
    'created_by_id': fields.String,
    'description': fields.String,
    'finished': fields.DateTime,
    'fleet_schedule_guid': fields.String,
    'id': fields.Integer,
    'message': fields.String,
    'mission': fields.String,
    'mission_id': fields.String,
    'ordered': fields.DateTime,
    'parameters': fields.List(fields.Arbitrary),
    'priority': fields.Integer,
    'started': fields.DateTime,
    'state': fields.String,
    'allowed_methods': fields.List(fields.String)
})

# TODO: find the real values
state_aborted = 'Aborted'
state_done = 'Done'
state_pending = 'Pending'
state_running = 'Running'


class MissionQueueItem:
    def __init__(self, i, priority, fleet_schedule_guid, message, mission_id, parameters):
        self.id = i
        self.priority = priority
        self.fleet_schedule_guid = fleet_schedule_guid
        self.message = message
        self.mission_id = mission_id
        self.state = state_pending
        self.control_state = 0  # TODO: change to 1 when the mission waits for something
        self.parameters = parameters
        self.control_posid = None
        self.started = None
        self.finished = None
        self.ordered = None
        self.allowed_methods = ['PUT', 'GET', 'DELETE']

    def __str__(self):
        return str(self.__class__) + ': ' + str(self.__dict__)

    def __lt__(self, other):
        return self.priority < other.priority if self.priority != other.priority else self.id < other.id

    def stop(self):
        if self.state in [state_pending[0], state_running[0]]:
            self.state = 'Aborted'

    def get_actions(self):
        return '/v2.0.0/mission_queue/{}/actions'.format(self.id)

    actions = property(get_actions)

    def get_mission(self):
        return '/v2.0.0/missions/{}'.format(self.mission_id)

    mission = property(get_mission)


class MissionQueueDAOClass:
    def __init__(self):
        self.pending_queue = []
        self.all_queue = []
        self.current_i = 0

    def add(self, post_data):
        if 'mission_id' not in post_data:
            api.abort(400, 'mission_id is required')
            return
        self.current_i += 1
        item = MissionQueueItem(
            self.current_i,
            post_data.get('priority', 0),
            post_data.get('fleet_schedule_guid', ''),
            post_data.get('message', ''),
            post_data['mission_id'],
            post_data.get('parameters', []))
        heappush(self.pending_queue, item)
        self.all_queue.append(item)
        # TODO: start the mission here if they do something
        return item

    def stop_all(self):
        # TODO: if the missions do something, stop them
        map(lambda item: item.stop(), self.all_queue)

    def stop(self, i):
        # TODO: if the missions do something, stop them
        self.all_queue[i-1].stop()

    def get(self, i):
        if 0 <= i <= self.current_i:
            return self.all_queue[i-1]
        else:
            api.abort(404, "Mission with id {} doesn't exist".format(i))


missionQueueDAO = MissionQueueDAOClass()


@api.route('/json/v2.0.0/mission_queue')
class MissionQueue(Resource):
    @auth_required
    @ns.marshal_with(getMissionQueues_model)
    def get(self):
        return missionQueueDAO.all_queue

    @auth_required
    @ns.expect(postMissionQueues_model)
    def post(self):
        if api.payload is not None:
            return missionQueueDAO.add(api.payload)
        api.abort(400, 'Request body needs to be application/json')

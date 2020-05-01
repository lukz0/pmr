from models.mission import *

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
        return MissionQueueDAO.get(i), 200

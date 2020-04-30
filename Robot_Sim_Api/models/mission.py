from app import *

getMission_model = api.model('GetMission', {
    'guid': fields.String,
    'name': fields.String,
    'url': fields.String
})


class SingleMission:
    def __init__(self, guid, name):
        self.guid = guid
        self.name = name

    def __str__(self):
        return str(self.__class__) + ': ' + str(self.__dict__)

    def get_url(self):
        return '/v2.0.0/missions/{}'.format(self.guid)

    url = property(get_url)


class MissionsDAOClass:
    def __init__(self, mission_arr):
        self.arr = mission_arr
        self.mission_i = {}
        for i, v in enumerate(self.arr):
            self.mission_i[v.guid] = i

    def get(self, guid):
        if guid in self.mission_i:
            return self.arr[self.mission_i[guid]]
        else:
            api.abort(404, "Mission with guid {} doesn't exist".format(guid))

    def __str__(self):
        return str(self.__class__) + ': ' + str(self.__dict__)


missionsDAO = MissionsDAOClass([
    SingleMission('mirconst-guid-0000-0001-actionlist00', 'Move'),
    SingleMission('mirconst-guid-0000-0003-actionlist00', 'GoToPositionPrototype'),
    SingleMission('mirconst-guid-0000-0004-actionlist00', 'ChargeAtStation'),
    SingleMission('mirconst-guid-0000-0006-actionlist00', 'StageAtPosition'),
    SingleMission('mirconst-guid-0000-0005-actionlist00', 'Dock'),
    SingleMission('mirconst-guid-0000-0007-actionlist00', 'PickupCart'),
    SingleMission('mirconst-guid-0000-0008-actionlist00', 'PlaceCart'),
    SingleMission('1727f99d-1671-11ea-a892-94c69118fd1e', 'Frem og tilbake'),
    SingleMission('42d851ba-40fa-11ea-a313-94c69118fd1e', 'Tute tur'),
    SingleMission('9aca0130-82fc-11ea-8e4d-94c69118fd1e', 'Test2'),
    SingleMission('74c6adc6-839f-11ea-bfdf-94c69118fd1e', 'A til B'),
    SingleMission('a784d483-83a7-11ea-bfdf-94c69118fd1e', 'Kont'),
    SingleMission('f6d6607a-845d-11ea-a732-94c69118fd1e', 'Gruppe 5 hall 3'),
    SingleMission('1a29c894-8463-11ea-a732-94c69118fd1e', 'Hall 3 gruppe 5'),
    SingleMission('5566ea90-848a-11ea-856f-94c69118fd1e', 'Lagersjefen')
])


@api.route('/json/v2.0.0/missions/')
class Missions(Resource):
    @ns.marshal_with(getMission_model)
    def get(self):
        return missionsDAO.arr, 200


@api.route('/json/v2.0.0/missions/<string:guid>')
@ns.param('guid', 'Mission guid')
class MissionsGuid(Resource):
    @ns.marshal_with(getMission_model)
    def get(self, guid):
        return missionsDAO.get(guid), 200

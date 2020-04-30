from app import *

registers_model = api.model('register', {
    'id': fields.Integer,
    'label': fields.String,
    'url': fields.String,
    'value': fields.Float
})


class SingleRegister:
    def __init__(self, i, label, value):
        self.id = i
        self.label = label
        if i > 200:
            self.value = value
        else:
            self.value = float(int(value))
        self.url = '/v2.0.0/registers/{}'.format(i)

    def __str__(self):
        return str(self.__class__) + ': ' + str(self.__dict__)


class RegisterDAOClass:
    def __init__(self):
        self.dict = {}
        for i in range(1, 201):
            self.dict[i] = SingleRegister(i, '', 0.0)

    def get(self, i):
        if i in self.dict:
            return self.dict[i]
        else:
            api.abort(404, "Register {} doesn't exist".format(i))

    def set(self, i, data):
        if i in self.dict:
            if 'label' in data:
                self.dict[i].label = data['label']
            if 'value' in data:
                self.dict[i].value = data['value']
            return self.dict[i]
        else:
            api.abort(404, "Register {} doesn't exist".format(i))

    def __str__(self):
        return str(self.__class__) + ': ' + str(self.__dict__)


RegisterDAO = RegisterDAOClass()


@api.doc(responses={400: 'Invalid ordering or Invalid filters or Wrong output fields or Invalid limits'})
@api.doc(responses={404: 'Not found'})
@api.doc(responses={202: 'Successfully retrieve the specified element'})
@api.route('/json/v2.0.0/registers/')
class Register(Resource):
    @ns.marshal_with(registers_model)
    def get(self):
        return list(RegisterDAO.dict.values())


@api.route('/json/v2.0.0/registers/<int:i>')
@ns.param('i', 'Register id')
class RegisterID(Resource):
    @ns.marshal_with(registers_model)
    def get(self, i):
        return RegisterDAO.get(i)

    @ns.expect(registers_model)
    @ns.marshal_with(registers_model)
    def put(self, i):
        print(api.payload)
        return RegisterDAO.set(i, api.payload)

    @ns.expect(registers_model)
    @ns.marshal_with(registers_model)
    def post(self, i):
        print(api.payload)
        return RegisterDAO.set(i, api.payload)

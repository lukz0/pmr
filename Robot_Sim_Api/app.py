from flask import Flask
from flask_restplus import Api, fields, Resource

from werkzeug.exceptions import *

robot_base_url = '/api/v2.0.0'
auth = {
    'apikey': {
        'type': 'apiKey',
        'in': 'header',
        'name': 'X-API-KEY'
    }
}

app = Flask(__name__)
api = Api(app,
          version='1.0',
          title='Robot API',
          description='Simulation API for MiR Robot',
          default_mediatype='application/json',
          authorizations=auth,
          contact_email='byamungukabiraba@gmail.com')

ns = api.namespace('Robot API', description='TODO operations')



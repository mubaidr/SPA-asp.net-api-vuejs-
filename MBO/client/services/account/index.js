module.exports = {
  signup: signup,
  signin: signin
}
import axios from 'axios'
import api from 'api'

function signup(obj) {
  return axios.post(api.url + '/account/register', obj);
}

function signin(obj) {
  return axios.post(api.url + '/account/login', obj);
}

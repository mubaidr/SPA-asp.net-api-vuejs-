module.exports = {
  signup: signup
}
import axios from 'axios'
import api from 'api'

function signup(obj) {
  console.log(obj);
  return axios.post(api.url + '/account/register', obj);
}

function signin(obj) {
  console.log(obj);
  return axios.post(api.url + '/account/login', obj);
}

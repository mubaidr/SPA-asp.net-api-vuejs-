export {
  signup,
  signin
}
import api from 'api'
import axios from 'utilities/axios'
import qs from 'qs'

function signup(obj) {
  return axios.post(api.url + '/api/account/register', obj);
}

function signin(obj) {
  obj['grant_type'] = 'password';
  return axios.post(api.url + '/Token', qs.stringify(obj));
}

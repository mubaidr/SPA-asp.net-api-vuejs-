export {
  GetAllContacts,
  getContacts,
  get,
  post
}

import api from 'api'
import axios from 'utilities/axios'

const path = '/api/Messages'

function GetAllContacts () {
  return axios.post(api.url)
}

function getContacts (obj) {
  return axios.post(api.url + path, obj)
}

function get (obj) {
  return axios.get(api.url + path, obj)
}

function post (obj) {
  return axios.post(api.url + path, obj)
}

// function remove (obj) {
//   return axios.post(`${api.url + path}/Remove/?id=${obj.id}`, obj)
// }

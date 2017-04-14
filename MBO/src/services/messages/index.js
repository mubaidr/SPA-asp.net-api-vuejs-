export {
  getAllContacts,
  getContacts,
  getMessages,
  postMessage
}

import api from 'api'
import axios from 'utilities/axios'

const path = '/api/Messages'

function getAllContacts () {
  return axios.get(api.url + path + '/getAllContacts')
}

function getContacts () {
  return axios.get(api.url + path + '/getContacts')
}

function getMessages (obj) {
  return axios.get(api.url + path + '?folder=' + obj.folder)
}

function postMessage (obj) {
  return axios.post(api.url + path, obj)
}

// function remove (obj) {
//   return axios.post(`${api.url + path}/Remove/?id=${obj.id}`, obj)
// }

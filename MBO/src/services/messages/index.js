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

function getContacts (obj) {
  return axios.get(api.url + path + '/getContacts', obj)
}

function getMessages (obj) {
  return axios.get(api.url + path, obj)
}

function postMessage (obj) {
  return axios.post(api.url + path, obj)
}

// function remove (obj) {
//   return axios.post(`${api.url + path}/Remove/?id=${obj.id}`, obj)
// }

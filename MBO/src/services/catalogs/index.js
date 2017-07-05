import api from 'api'
import axios from 'axios'

export {
  getCategories,
  getStatus
}

function getCategories () {
  return axios.get(`${api.url}/api/Categories`)
}

function getStatus () {
  return axios.get(`${api.url}/api/Status`)
}

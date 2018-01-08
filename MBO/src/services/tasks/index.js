import api from 'api'
// eslint-disable-next-line
import axios from 'utilities/axios'

const path = '/api/MainTasks'

export default {
  create (obj) {
    return axios.post(api.url + path, obj)
  },

  getDetails (obj) {
    return axios.get(api.url + path, obj)
  },

  edit (obj) {
    return axios.post(api.url + path, obj)
  },

  remove (obj) {
    return axios.post(`${api.url + path}/Remove/?id=${obj.id}`, obj)
  },

  restore (obj) {
    return axios.post(`${api.url + path}/Restore/?id=${obj.id}`, obj)
  },

  delete (obj) {
    return axios.post(api.url + path, obj)
  },

  list (type, obj) {
    return axios.get(`${api.url + path}?type=${type}${this.objToQuery(obj)}`)
  },

  objToQuery (obj) {
    let query = ''
    let val = ''
    // eslint-disable-next-line
    for (const prop in obj) {
      val = obj[prop]
      if ((val && (val.trim && val.trim() !== '')) || val > 0) {
        query += `&${prop}=${val}`
      }
    }
    return query
  }
}

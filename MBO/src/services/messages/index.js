import api from 'api'
// eslint-disable-next-line
import axios from 'utilities/axios'

const path = '/api/Messages'

export default {
  getAllContacts () {
    return axios.get(`${api.url + path}/getAllContacts`)
  },

  getContacts () {
    return axios.get(`${api.url + path}/getContacts`)
  },

  getMessage (obj) {
    return axios.get(
      `${api.url + path}?folder=${obj.folder}${this.objToQuery(obj)}`
    )
  },

  postMessage (obj) {
    return axios.post(api.url + path, obj)
  },

  deleteMessage (obj) {
    return axios.post(`${api.url + path}/Delete?Id=${obj}`)
  },

  restoreMessage (obj) {
    return axios.post(`${api.url + path}/Restore?Id=${obj}`)
  },

  markReadMessage (obj) {
    return axios.post(`${api.url + path}/Read?Id=${obj}`)
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

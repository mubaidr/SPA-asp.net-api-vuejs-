import api from 'api'
import axios from 'utilities/axios'

const path = '/api/Messages'

export default {

  getAllContacts () {
    return axios.get(api.url + path + '/getAllContacts')
  },

  getContacts () {
    return axios.get(api.url + path + '/getContacts')
  },

  get (obj) {
    return axios.get(api.url + path + '?folder=' + obj.folder + this.objToQuery(obj))
  },

  post (obj) {
    return axios.post(api.url + path, obj)
  },

  delete (obj) {
    return axios.post(api.url + path + '/Delete?Id=' + obj)
  },

  restore (obj) {
    return axios.post(api.url + path + '/Restore?Id=' + obj)
  },

  markRead (obj) {
    return axios.post(api.url + path + '/Read?Id=' + obj)
  },

  objToQuery (obj) {
    let query = ''
    let val = ''
    for (const prop in obj) {
      val = obj[prop]
      if ((val && (val.trim && val.trim() !== '')) || val > 0) {
        query += `&${prop}=${val}`
      }
    }
    return query
  }

}

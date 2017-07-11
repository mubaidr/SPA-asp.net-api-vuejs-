import api from 'api'
import axios from 'axios'

export default {

  add (obj) {
    return axios.post(`${api.url}/api/ProgressHistories`)
  },

  get (id) {
    // TODO get ProgressHistory based on task id
    return axios.get(`${api.url}/api/ProgressHistories?mainTaskID=${id}`)
  }
}

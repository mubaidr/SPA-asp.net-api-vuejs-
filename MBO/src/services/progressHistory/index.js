import api from 'api'
import axios from 'axios'

export default {

  addProgressHistory (obj) {
    return axios.post(`${api.url}/api/ProgressHistories`)
  },

  getProgressHistory (id) {
    // TODO get ProgressHistory based on task id
    return axios.get(`${api.url}/api/ProgressHistories?mainTaskID=${id}`)
  }
}

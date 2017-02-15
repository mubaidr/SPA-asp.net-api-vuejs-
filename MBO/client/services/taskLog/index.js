export {
  addLog,
  getLog
};

import api from 'api';
import axios from 'axios';

function addLog(obj) {
  return axios.post(api.url + '/api/Logs');
};

function getLog(id) {
  return axios.get(api.url + '/api/Logs?mainTaskID=' + id);
};

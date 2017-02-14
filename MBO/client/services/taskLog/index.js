export {
  addLog,
  getLog
};

import api from 'api';
import axios from 'axios';

function addLog(obj) {
  return axios.post(api.url + '/api/Log');
};

function getLog() {
  return axios.get(api.url + '/api/Log');
};

export {
  addProgressHistory,
  getProgressHistory
};

import api from 'api';
import axios from 'axios';

function addProgressHistory() {
  return axios.post(api.url + '/api/ProgressHistories');
};

function getProgressHistory() {
  return axios.get(api.url + '/api/ProgressHistories');
};

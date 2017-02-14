export {
  getProgressHistory
};

import api from 'api';
import axios from 'axios';

function getProgressHistory() {
  return axios.get(api.url + '/api/ProgressHistory');
};

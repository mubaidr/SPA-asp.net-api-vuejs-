import axios from 'axios'
//TODO intercept auth fail requst and redirect to login with path
axios.interceptors.response.use(function (response) {
  return response;
}, function (error) {
  return Promise.reject(error);
});

export default axios

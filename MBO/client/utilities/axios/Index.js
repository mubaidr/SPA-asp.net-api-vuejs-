import axios from 'axios';
import router from 'utilities/router';
import session from 'utilities/session';

//TODO set auth header
axios.interceptors.request.use(function (config) {
  return config;
}, function (error) {
  return Promise.reject(error);
});

//TODO redirect to login if auth failed or expired
axios.interceptors.response.use(function (response) {
  return response;
}, function (error) {
  return Promise.reject(error);
});

export default axios;

import axios from 'axios';
import router from 'utilities/router';
import store from 'utilities/store';
import session from 'utilities/session';

axios.interceptors.request.use(function (config) {

  store.commit('setState', {
    loading: true,
    err: null
  });

  if (store.getters.isAuhtenticated) {
    const auth = store.getters.getAuth;
    config.headers.Authorization = auth.token_type + ' ' + auth.access_token;
  }

  return config;
}, function (error) {

  store.commit('setState', {
    loading: false,
    err: error
  });

  return Promise.reject(error);
});

//TODO redirect to login if auth failed or expired
axios.interceptors.response.use(function (response) {

  store.commit('setState', {
    loading: false,
    err: null
  });

  return response;
}, function (error) {

  store.commit('setState', {
    loading: false,
    err: error
  });

  return Promise.reject(error);
});

export default axios;

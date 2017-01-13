import Vue from 'vue';
import Vuex from 'vuex';
import router from 'utilities/router';
import session from 'utilities/session';

Vue.use(Vuex);

const state = {
  auth: session.getAuth(),
  userinfo: session.getUserInfo()
};

const mutations = {
  setAuthentication(state, auth) {
    state.auth = auth;
    session.setAuth(auth);
  },
  setUserInfo(state, userinfo) {
    state.userinfo = userinfo;
    session.setUserInfo(userinfo);
  },
  removeAuthentication(state) {
    state.auth = null;
    state.userinfo = null;
    session.clear();
  }
};

const actions = {};

const getters = {
  isAuhtenticated: function (state) {
    return state.auth !== null && typeof state.auth !== 'undefined';
  },
  getUserInfo: function (state) {
    return state.userinfo;
  },
  getAuth: function (state) {
    return state.auth;
  }
};

const store = new Vuex.Store({
  state,
  mutations,
  actions,
  getters
});

export default store;

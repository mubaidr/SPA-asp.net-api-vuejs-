import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

const state = {
  request: {
    loading: false,
    valid: true
  },
  page: {
    success: true,
    message: 'test',
    details: ['test', 'test']
  }
}

const mutations = {
  toggleValidity(state) {
    state.request.valid = !state.request.valid;
  },
  isValid(state) {
    state.request.valid = true;
  },
  isNotValid(state) {
    state.request.valid = false;
  },
  toggleLoading(state) {
    state.request.loading = !state.request.loading;
  },
  isLoading(state) {
    state.request.loading = true;
  },
  isNotLoading(state) {
    state.request.loading = false;
  },
  setResponse(state, err) {
    state.request.err = err;
  },
  setState(state, request) {
    if (typeof request.loading === 'undefined') {
      request.loading = state.request.loading;
    }
    if (typeof request.valid === 'undefined') {
      request.valid = state.request.valid;
    }
    if (typeof request.err === 'undefined') {
      //request.err = state.request.err;
      //TODO update page with new messages and details
    }
    state.request = request;
  }
}

const actions = {}

//TODO add getter for error object (list and heading)
const getters = {
  request: function (state) {
    return state.request;
  },
  page: function (state) {
    return state.page;
  }
}

const store = new Vuex.Store({
  state,
  mutations,
  actions,
  getters
})

export default store

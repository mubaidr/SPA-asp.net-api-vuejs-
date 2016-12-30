import Vue from 'vue'
import Vuex from 'vuex'
import router from 'utilities/router'
import session from 'utilities/session'

Vue.use(Vuex)

const state = {
  auth: session.get(),
  page: {
    loading: false,
    alert: false,
    type: 'info', //info, success, fail
    message: '',
    details: []
  }
}

const mutations = {
  setAuthentication(state, auth) {
    state.auth = auth;
    session.set(auth);
  },
  removeAuthentication(state) {
    state.auth = null;
    session.clear();
  },
  isLoading(state) {
    state.page.loading = true;
  },
  isNotLoading(state) {
    state.page.loading = false;
  },
  clearState(state) {
    state.page.loading = false;
    state.page.alert = false;
    state.page.type = 'info';
    state.page.message = '';
    state.page.details.length = 0;
  },
  setMessage(state, obj) {
    state.page.type = obj.type;
    state.page.alert = true;
    switch (obj.message) {
      case 'signup-success':
        state.page.message = 'You have succeesfully signed up!';
        break;
    }
  },
  setState(state, obj) {
    if (obj.err) {
      state.page.type = 'error';
      state.page.alert = true;

      var err = obj.err;
      state.page.details.length = 0;
      if (!err.response || !err.response.data) {
        state.page.message = "Unable to contact server!";
      } else if (typeof err.response.data.error === 'string') {
        state.page.message = err.response.data.error_description;
      } else if (typeof err.response.data.ModelState === 'object') {
        //state.page.message = 'Please fix following errors: ';
        var _model_state = err.response.data.ModelState;

        Object.keys(_model_state).forEach(function (key) {
          var val = _model_state[key];
          if (typeof val === "object" && val.length) {
            val.forEach(function (msg) {
              state.page.details.push(msg);
            });
          } else {
            state.page.details.push(val);
          }
        });

      } else {
        state.page.message = "Something went wrong!";
      }
    } else {
      state.page.loading = obj.loading;
      state.page.alert = false;
    }
  }
}

const actions = {}

const getters = {
  isAuhtenticated: function (state) {
    return state.auth !== null && typeof state.auth !== 'undefined';
  },
  isLoading: function (state) {
    return state.page.loading;
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

import Vue from 'vue'

var mixin = {
  methods: {
    setErrorDetails (error) { // eslint-disable-next-line
      console.dir(error)
    }
  }
}

Vue.config.errorHandler = function (err, vm, info) { // eslint-disable-next-line
  console.log('______Error: _____', err, vm, info)
}

Vue.mixin(mixin)
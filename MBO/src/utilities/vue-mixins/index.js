import Vue from 'vue'

var mixin = {
  methods: {
    setErrorDetails (error) {
      console.dir(error)
    }
  }
}

Vue.config.errorHandler = function (err, vm, info) {
  console.log('______Error: _____', err, vm, info)
}

Vue.mixin(mixin)

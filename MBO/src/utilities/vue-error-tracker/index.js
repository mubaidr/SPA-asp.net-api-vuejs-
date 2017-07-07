import Vue from 'vue'
import vueErrorTracker from '@deveodk/vue-error-tracker'

Vue.use(vueErrorTracker, {
  HttpDriver: require('@deveodk/vue-error-tracker/drivers/http/axios.js'),
  NotificationDriver: require('@deveodk/vue-error-tracker/drivers/notification/deveoToastr.js'),
  HttpErrorCodes: {
    400: {
      title: 'Unknown error',
      message: 'Something went wrong',
      type: 'error'
    },
    500: {
      title: 'Server error',
      message: 'Ohh no something went wrong',
      type: 'error'
    }
  }
})

import Vue from 'vue'
import CxltToastr from 'cxlt-vue2-toastr'
import 'cxlt-vue2-toastr/dist/css/cxlt-vue2-toastr.css'

var config = {
  position: 'bottom right',
  showMethod: 'slideInRight',
  hideMethod: 'slideOutRight',
  timeOut: 7500,
  showDuration: 250,
  hideDuration: 250
}

Vue.use(CxltToastr, config)

import Vue from 'vue'
import CxltToastr from 'cxlt-vue2-toastr'
import 'cxlt-vue2-toastr/dist/css/cxlt-vue2-toastr.css'

const config = {
  position: 'bottom right',
  showMethod: 'slideInUp',
  hideMethod: 'slideOutDown',
  timeOut: 7500,
  showDuration: 250,
  hideDuration: 250
}

Vue.use(CxltToastr, config)

<template>
  <div class="form-message" :class="page.type" v-show="page.alert">
    <!--<pre>{{page}}</pre>-->
    <p>{{page.message}}</p>
    <ul v-show="page.details.length">
      <li v-for="detail in page.details">
        {{detail}}
      </li>
    </ul>
  </div>
</template>
<script>
  export default {
    //   page: {
    //   loading: false,
    //   alert: false,
    //   type: 'info', //info, success, fail
    //   message: '',
    //   details: []
    // }
    name: 'app-message',
    prop: ['page'],
    computed: {
      page: function () {
        return this.$store.getters.page;
      }
    },
    methods: {
      parseError: function () {
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
            state.page.message =
              "Something went wrong! If the problem persists, please contact system administrator.";
          }
        } else {
          state.page.alert = false;
        }
      }
    }
  }

</script>
<style scoped>
  .form-message.info {
    color: #4c1f50;
  }
  
  .form-message.success {
    color: #4caf50;
  }
  
  .form-message.fail {
    color: #ff5722;
  }
  
  .form-message ul {
    list-style: none;
    padding-left: 0px;
  }

</style>

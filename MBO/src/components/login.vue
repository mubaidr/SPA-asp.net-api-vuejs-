<template id="login">
  <div>
    <h1>Login</h1>
    <div class="row">
      <div class="col-md-6">
        <form class="form-horizontal">
          <fieldset>
            <legend>Please enter username and password to continue.</legend>
            <div class="form-group">
              <label for="Username" class="col-lg-4 control-label">Username</label>
              <div class="col-lg-8">
                <input v-model="username" type="text" class="form-control" id="Username" placeholder="Username">
              </div>
            </div>
            <div class="form-group">
              <label for="Password" class="col-lg-4 control-label">Password</label>
              <div class="col-lg-8">
                <input v-model="password" type="password" class="form-control" id="Password" placeholder="Password">
                <div class="checkbox">
                  <label>
                    <input type="checkbox"/> Remember me
                  </label>
                </div>
              </div>
            </div>
            <div class="form-group">
              <div class="col-md-offset-4 col-md-8">
                <button v-on:click="login" class="btn btn-primary btn-block">Login to my account</button>
              </div>
            </div>
            <div class="row">
              <div class="col-md-offset-4 col-md-8">
                Don't have a account?
                <router-link to="/signup">Signup here!</router-link>
              </div>
            </div>
            <div class="row">
              <div class="col-md-offset-4 col-md-8">
                Forgot password?
                <router-link to="/recover">Recover now!</router-link>
              </div>
            </div>
          </fieldset>
        </form>
      </div>
    </div>
  </div>
</template>
<script>
  export default {
    template: '#login',
    name: 'login',
    data: function (){
      return {
        username: 'test',
        password: 'asdf1234'
      }
    },
    methods:{
      login: function(event){
        event.preventDefault();
        this.$http.get('http://localhost/Beta/api/values').then((response) => {
          console.log('success', response);
        }, (response) => {
          console.log('fail', response);
        });
      }
    }
  }
</script>

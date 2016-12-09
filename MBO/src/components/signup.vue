<template id="signup">
  <div>
    <h1>Signup</h1>
    <div class="row">
      <div class="col-md-6">
        <form class="form-horizontal">
          <fieldset>
            <legend>Please provide following details to signup.</legend>
            <div class="form-group">
              <label for="Username" class="col-lg-4 control-label">Username</label>
              <div class="col-lg-8">
                <input v-model="username" type="text" class="form-control" placeholder="Username">
              </div>
            </div>
            <div class="form-group">
              <label for="Password" class="col-lg-4 control-label">Password</label>
              <div class="col-lg-8">
                <input v-model="password" type="password" class="form-control" placeholder="Password">
              </div>
            </div>
            <div class="form-group">
              <label for="ConfirmPassword" class="col-lg-4 control-label">Confirm Password</label>
              <div class="col-lg-8">
                <input v-model="confirmPassword" type="password" class="form-control" placeholder="Confirm Password">
              </div>
            </div>
            <div class="form-group">
              <div class="col-md-offset-4 col-md-8">
                <button v-on:click="register" class="btn btn-primary btn-block">Create Account</button>
              </div>
            </div>
            <div class="row">
              <div class="col-md-offset-4 col-md-8">
                Already have an account?
                <router-link to="/login">Signin here!</router-link>
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
    template: '#signup',
    name: 'signup',
    data: function (){
      return {
        username: 'test',
        password: 'asdf1234',
        confirmPassword: 'asdf1234'
      }
    },
    methods:{
      register: function(event){
        event.preventDefault();
        this.$http.post('http://localhost/Beta/api/token').then((response) => {
          console.log('success', response);
        }, (response) => {
          console.log('fail', response);
        });
      }
    }
  }
</script>

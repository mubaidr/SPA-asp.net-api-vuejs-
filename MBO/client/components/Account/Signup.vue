<template id="signup">
  <div>
    <div class="row">
      <div class="col-md-offset-4 col-md-4">
        <form>
          <h1>Signup</h1>
          <p>Please provide following details to signup.</p>
          <div class="alert alert-dismissible alert-danger">
            <button type="button" class="close" data-dismiss="alert">&times;</button>
            <strong>Oh snap!</strong> Change a few things up and try submitting again.
          </div>
          <div class="form-group">
            <label for="Username" class="control-label">Username</label>
            <input v-model="username" type="text" class="form-control" placeholder="Username">
          </div>
          <div class="form-group">
            <label for="Password" class="control-label">Password</label>
            <input v-model="password" type="password" class="form-control" placeholder="Password">
          </div>
          <div class="form-group">
            <label for="ConfirmPassword" class="control-label">Confirm Password</label>
            <input v-model="confirmPassword" type="password" class="form-control" placeholder="Confirm Password">
          </div>
          <div class="form-group">
            <button v-on:click="register" class="btn btn-primary btn-block">Create Account</button>
          </div>
          <div class="form-group">
            Already have an account?
            <router-link to="/login">Signin here!</router-link>
            <br/> Forgot password?
            <router-link to="/recover">Recover now!</router-link>
          </div>
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

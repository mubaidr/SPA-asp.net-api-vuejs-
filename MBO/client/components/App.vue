<template id="app">
  <div>
    <nav class="navbar navbar-inverse">
      <div class="container-fluid">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#header-navbar">
        <span class="sr-only">Toggle navigation</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
          <router-link to="/home" class="navbar-brand">MBO</router-link>
        </div>
        <div class="collapse navbar-collapse" id="header-navbar">
          <ul class="nav navbar-nav">
            <li>
              <router-link to="/about">About</router-link>
            </li>
            <li>
              <router-link to="/contact">Contact</router-link>
            </li>
          </ul>
          <form class="navbar-form navbar-left " role="search ">
            <div class="form-group ">
              <input type="text " class="form-control " placeholder="Search ">
            </div>
          </form>
          <ul class="nav navbar-nav navbar-right ">
            <li>
              <router-link to="/login">Login</router-link>
            </li>
            <li>
              <router-link to="/signup">Signup</router-link>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    <div class="container">
      <transition name="slide-fade" mode="out-in">
        <router-view></router-view>
      </transition>
    </div>
    <footer class="footer">
      <div class="container-fluid">
        <p class="text-muted">Place sticky footer content here.</p>
      </div>
    </footer>
  </div>
</template>
<script>
  export default{

  }
</script>

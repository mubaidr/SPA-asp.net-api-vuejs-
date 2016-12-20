<template>
  <md-toolbar>
    <div class="md-toolbar-container">
      <md-button class="md-icon-button">
        <md-icon>menu</md-icon>
      </md-button>
      <span style="flex: 1;">Home</span>
      <md-button class="md-icon-button">
        <md-icon>search</md-icon>
      </md-button>
      <md-button class="md-icon-button">
        <md-icon>filter_list</md-icon>
      </md-button>
      <md-menu md-direction="bottom left">
        <md-button md-menu-trigger>Bottom Left</md-button>
        <md-menu-content>
          <md-menu-item>My Item 1</md-menu-item>
          <md-menu-item>My Item 2</md-menu-item>
          <md-menu-item disabled>My Item 3</md-menu-item>
          <md-menu-item>My Item 4</md-menu-item>
        </md-menu-content>
      </md-menu>
    </div>
  </md-toolbar>
</template>
<script>
  export default {

  }

</script>

<template>
  <div>    
    <md-whiteframe md-tag="section" class="full-width">
      <md-toolbar>
        <md-button class="md-icon-button" @click.native="$router.push({ path: '/tasks' })">
          <md-tooltip md-direction="top">Tasks</md-tooltip>
          <md-icon>arrow_back</md-icon>
        </md-button>
        <span class="md-headline">Create Task</span>
        <span style="flex: 1"></span>
        <md-button @click.native="$router.push({path: '/dashboard'})">
          <md-tooltip md-direction="top">Dashboard</md-tooltip>
          Dashboard
        </md-button>
      </md-toolbar>      
    </md-whiteframe>
    <br/>
    <md-layout>
      <md-layout md-hide-small></md-layout>
      <md-layout>
        <task-create></task-create>
      </md-layout>
      <md-layout md-hide-small>
      </md-layout>
    </md-layout>
  </div>
</template>
<script>
  import taskCreate from 'components/Management/Tasks/_partial_create.vue'

  export default {
    components: {
      'task-create': taskCreate
    }
  }

</script>

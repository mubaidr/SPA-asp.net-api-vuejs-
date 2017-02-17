<template>
  <div>
    <span class="md-display-1">Tasks</span>
    <p>
      <router-link class="md-accent" :to="{path: '/tasks'}">
        View All Tasks
      </router-link>,
      <router-link class="md-accent" :to="{path: '/tasks/trash'}">
        View Trash
      </router-link>
    </p>
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

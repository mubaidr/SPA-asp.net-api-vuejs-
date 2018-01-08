<template>
  <md-layout>
    <md-layout></md-layout>
    <md-layout md-flex-large="33" md-flex-medium="50" md-flex-small="75" md-flex-xsmall="100">
      <task-create></task-create>
    </md-layout>
    <md-layout></md-layout>
  </md-layout>
</template>
<script>
  import taskCreate from 'components/Management/Tasks/_partial_create.vue'

  export default {
    components: {
      taskCreate
    }
  }
</script>

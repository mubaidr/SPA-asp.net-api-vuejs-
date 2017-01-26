<template>
  <md-layout md-flex="20" md-flex-xsmall="100" md-flex-small="50" md-flex-medium="33" class="card">
    <!--Card Details if needed-->
  </md-layout>
</template>
<script>
  import moment from 'moment';

  export default {
    name: 'task-card',
    props: ['Task'],
    computed: {
      type_class: function () {
        const _self = this;

        const now = moment();
        const dueDate = moment(_self.Task.DateDue);
        const diff = now.diff(dueDate, 'days');

        if (diff < 0) {
          return 'md-warn';
        } else if (diff < 2) {
          return 'md-primary';
        } else {
          return '';
        }
      },
      formatedDueDate: function () {
        return moment(this.Task.DateDue).format('HH:mm A [,] DD-MM-YYYY');
      }
    },
    methods: {

    }
  }

</script>
<style scoped>
  .card {
    margin-bottom: 20px;
  }
  
  .md-card-custom {
    width: 95%;
    overflow-x: hidden;
  }

</style>

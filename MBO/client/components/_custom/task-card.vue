<template>
  <md-layout md-flex="20">
    <md-card md-with-hover class="full-width" :class="type_class">
      <md-card-area md-inset>
        <md-card-header>
          <div class="md-title">{{Task.Title}}</div>
          <div class="md-subhead">
            Due Date: {{Task.DateDue}}
            <br/> Assigned By: {{Task.AssignedBy.UserName}}
          </div>
        </md-card-header>
        <md-card-content>
          {{Task.Description}}
        </md-card-content>
      </md-card-area>
      <md-card-content>
        <h3 class="md-subheading">Today's availability</h3>
        <div class="card-reservation">
          <md-icon>access_time</md-icon>
          <md-button-toggle md-single class="md-button-group">
            <md-button>5:30PM</md-button>
            <md-button>7:30PM</md-button>
            <md-button>9:00PM</md-button>
          </md-button-toggle>
        </div>
      </md-card-content>
      <md-card-actions>
        <md-button>Action</md-button>
        <md-button>Action</md-button>
      </md-card-actions>
    </md-card>
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
        //TOFIX Date diff
        const now = moment();
        const dueDate = moment(_self.Task.DateDue, 'DD-MM-YYYY HH:mm A');
        const diff = dueDate.diff(now, 'days');
        console.log(_self.Task.DateDue, now, dueDate);

        if (diff < 0) {
          return 'md-danger';
        } else if (diff < 2) {
          return 'md-warn';
        } else {
          return 'md-primary';
        }
      }
    },
    methods: {

    }
  }

</script>
<style scoped>


</style>

<template>
  <md-layout md-flex="20" md-flex-xsmall="100" md-flex-small="50" md-flex-medium="33" class="card">
    <md-card md-with-hover class="md-card-custom">
      <md-card-area md-inset>
        <md-card-header>
          <md-card-header-text>
            <div class="md-title">{{Task.Title}}</div>
            <div class="md-subhead">
              Assigned By: {{Task.AssignedBy.UserName}}
              <br/> Assigned To: {{Task.AssignedTo}}
            </div>
          </md-card-header-text>
          <md-menu md-size="3" md-direction="bottom left">
            <md-button class="md-icon-button" md-menu-trigger>
              <md-icon>more_vert</md-icon>
            </md-button>
            <md-menu-content>
              <md-menu-item>
                <span>View Details</span>
                <md-icon>phone</md-icon>
              </md-menu-item>
              <md-menu-item>
                <span>Add Comment</span>
                <md-icon>message</md-icon>
              </md-menu-item>
            </md-menu-content>
          </md-menu>
        </md-card-header>
        <md-card-content>
          <div class="right-align">
            <md-icon class="md-warn" v-show="type_class == 'md-warn'">warning</md-icon>
            <md-icon class="md-accent" v-show="type_class != 'md-warn'">query_builder</md-icon>
          </div>
          <md-progress :class="type_class" :md-progress="Task.Progress"></md-progress>
          <span>{{Task.Description}}</span>
        </md-card-content>
      </md-card-area>
      <md-card-content>
        <div class="card-reservation right-align" title="Due Date">
          <md-icon>access_time</md-icon>
          <span>{{formatedDueDate}}</span>
        </div>
      </md-card-content>
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

        const now = moment();
        const dueDate = moment(_self.Task.DateDue);
        const diff = now.diff(dueDate, 'days');

        if (diff < 0) {
          return 'md-warn';
        } else if (diff < 2) {
          return 'md-primary';
        } else {
          return 'md-accent';
        }
      },
      formatedDueDate: function () {
        return moment(this.Task.DateDue).format('HH:mm A [-] DD-MM-YYYY');
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
    width: 99%;
    overflow-x: hidden;
  }

</style>

<template>
  <md-layout md-flex="20" md-flex-xsmall="100" md-flex-small="50" md-flex-medium="33" class="card">
    <md-card md-with-hover class="md-card-custom">
      <md-card-header>
        <md-card-header-text>
          <div class="md-title" v-on:click="viewDetails()">
            {{Task.Title}}</div>
          <div class="md-subhead" v-on:click="viewDetails()">
            {{Task.AssignedBy.Email}}
          </div>
        </md-card-header-text>
        <md-menu md-size="3" md-direction="bottom left">
          <md-button class="md-icon-button" md-menu-trigger>
            <md-icon>more_vert</md-icon>
          </md-button>
          <md-menu-content>
            <md-menu-item>
              <span>Add Comment</span>
              <md-icon>message</md-icon>
            </md-menu-item>
          </md-menu-content>
        </md-menu>
      </md-card-header>
      <md-card-content style="padding-bottom: 0">
        <span v-on:click="viewDetails()">{{Task.Description || "No Description Provided."}}</span>
      </md-card-content>
      <md-card-content>
        <div class="card-date" :class="type_class" title="Due Date">
          <span class="text-muted">{{formatedDueDate}}</span>
          <md-icon :class="type_animate" class="pull-right">{{type_icon}}</md-icon>
        </div>
        <md-progress :md-theme="type_class" :md-progress="Task.Progress"></md-progress>
      </md-card-content>
      <md-card-content>
        <div class="md-caption" v-show="Task.AssignedTo.length">
          <span class="chip-custom" v-for="user in Task.AssignedTo">{{user.Email}}</span>
        </div>
        <div class="md-caption" v-show="!Task.AssignedTo.length">
          <span class="chip-custom">Self</span>
        </div>
      </md-card-content>
      <md-card-actions>
        <div v-show="isSelfCreated">
          <md-button class="md-icon-button" @click="confirmDelete">
            <md-icon>delete</md-icon>
          </md-button>
          <md-button class="md-icon-button">
            <md-icon>mode_edit</md-icon>
          </md-button>
          <md-button class="md-icon-button">
            <md-icon>low_priority</md-icon>
          </md-button>
        </div>
        <md-button v-on:click="viewDetails()">
          <md-icon>expand_more</md-icon>
          Details
        </md-button>
      </md-card-actions>
    </md-card>
    <!--Delete Confirmation-->
    <!--Should be a sinlge dialog for all cards-->
    <md-dialog :md-close-to="DialogCloseTarget" :ref="refConfirm">
      <md-dialog-title>Delete Task</md-dialog-title>
      <md-dialog-content>Are you sure you want to move this task to trash?</md-dialog-content>
      <md-dialog-actions>
        <md-button class="md-primary" @click="onDeleteClose('cancel')">Wait... that was a mistake!</md-button>
        <md-button class="md-primary" @click="onDeleteClose('ok')">Sure</md-button>
      </md-dialog-actions>
    </md-dialog>
  </md-layout>
</template>
<script>
  import {
    remove
  } from 'services/tasks';
  import moment from 'moment';

  export default {
    name: 'task-card',
    props: ['Task'],
    data: function () {
      return {
        DialogCloseTarget: ''
      }
    },
    computed: {
      isSelfCreated: function () {
        return this.$store.getters.getUserInfo.Email == this.Task.AssignedBy.Email;
      },
      type_class: function () {
        const _self = this;

        const now = moment();
        const dueDate = moment(_self.Task.DateDue);
        const diff = now.diff(dueDate, 'days');

        if (_self.Task.Progress == 100) {
          return 'theme-success';
        }
        if (diff < 0) {
          return 'theme-danger';
        } else if (diff < 2) {
          return 'theme-warn';
        } else if (diff < 5) {
          return 'theme-normal';
        } else {
          return 'theme-primary';
        }
      },
      type_animate: function () {
        const _self = this;

        const now = moment();
        const dueDate = moment(_self.Task.DateDue);
        const diff = now.diff(dueDate, 'days');

        if (diff < 0) {
          return 'animate-danger';
        } else if (diff < 2) {
          return 'animate-warn';
        } else {
          return '';
        }
      },
      type_icon: function () {
        switch (this.type_class) {
          case 'theme-success':
            return 'done';
            break;
          case 'theme-danger':
            return 'warning';
            break;
          case 'theme-warn':
            return 'av_timer';
            break;
          case 'theme-normal':
            return 'timelapse';
            break;
          case 'theme-primary':
          default:
            return 'timer';
            break;
        }
      },
      formatedDueDate: function () {
        return moment(this.Task.DateDue).format('HH:mm A [-] DD-MM-YYYY');
      },
      refConfirm: function () {
        return 'ref-confirm-' + this.Task.MainTaskID;
      }
    },
    methods: {
      viewDetails: function () {},
      confirmDelete: function () {
        this.$refs[this.refConfirm].open();
      },
      onDeleteClose: function (type) {
        if (type == "ok") {
          this.$set(this, 'DialogCloseTarget', '#btn-view-trash');
          this.animateTrashButton();

          //TODO delete card
        } else {
          this.$set(this, 'DialogCloseTarget', null);
        }
        this.$refs[this.refConfirm].close();
      },
      animateTrashButton: function () {
        window.setTimeout(function () {
          document.getElementById('btn-view-trash').className += " animate-active";
          window.setTimeout(function () {
            document.getElementById('btn-view-trash').className = document.getElementById('btn-view-trash').className
              .replace(/(?:^|\s)animate-active(?!\S)/g, '');
          }, 250);
        }, 200);
      }
    },
    mounted: function () {}
  }

</script>
<style scoped>
  .card {
    margin-bottom: 10px;
  }
  
  .card-date {
    margin-bottom: 10px;
  }
  
  .md-card-custom {
    width: 98%;
    overflow-x: hidden;
  }
  
  .md-card-custom .md-card-content:last-child {
    padding-top: 0!important;
  }
  
  .md-caption {
    /*margin-top: 10px;*/
  }
  
  .md-caption ul {
    list-style: none;
    padding-left: 0;
    margin: 0;
    margin-top: 5px;
  }
  
  .text-muted {
    opacity: 0.6;
  }
  
  .chip-custom {
    background-color: rgba(0, 0, 0, 0.1);
    padding: 5px;
    border-radius: 2px;
    margin-right: 2px;
  }
  
  .theme-danger .md-icon {
    color: #f44336;
  }
  
  .theme-warn .md-icon {
    color: #ff5722;
  }
  
  .theme-normal .md-icon {
    color: #ff9800;
  }
  
  .theme-primary .md-icon {
    color: #cddc39;
  }
  
  .theme-success .md-icon {
    color: #4caf50;
  }

</style>

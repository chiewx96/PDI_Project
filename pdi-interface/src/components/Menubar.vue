<template>
  <div>
    <v-layout>
      <v-app-bar
        color="purple"
        elevate-on-scroll
        dense
      >
        <!-- <template v-slot:image>
          <v-img
            gradient="to top right, rgba(19,84,122,.8), rgba(128,208,199,.8)"
          ></v-img>
        </template> -->

        <v-app-bar-title>PDI-Feather-Tracking</v-app-bar-title>
        <template v-slot:prepend>
          <v-btn
            icon="mdi-home"
            to="/"
          ></v-btn>
        </template>
        <template v-slot:append v-if="isLoggedIn">
          <v-btn 
            icon="mdi-qrcode-scan"
            to="/scan"
          ></v-btn>
          <v-menu>
            <template v-slot:activator="{ props }">
              <v-btn
                icon
                v-bind="props"
              >
                <v-icon>mdi-account</v-icon>
              </v-btn>
            </template>

            <v-list>
              <v-list-item>
                <v-list-item-title @click="logout">Logout</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </template>
      </v-app-bar>
      <v-main>
        <slot name="main" />
      </v-main>
    </v-layout>
  </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex';
export default {
  name: 'MenuBar',
  props: {},
  computed: {
    ...mapActions(['logout']),
    ...mapGetters(['isLoggedIn']),
  },
  methods: {},
};
</script>

<template>
  <v-dialog v-model="dialog" activator="parent" width="auto">
    <v-card>
      <v-card-text v-text="bundle_info"> </v-card-text>
      <v-card-actions>
        <v-col>
          <v-btn color="primary" block @click="$emit('confirm_outbound')"
            >Confirm</v-btn
          >
        </v-col>
        <v-col>
          <v-btn color="danger" block @click="$emit('cancel_outbound')"
            >Close</v-btn
          >
        </v-col>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "bundle-info-component",
  props: {
    bundle_info: null,
    bundle_info_toggle: null,
  },
  computed: {
    dialog() {
      return this.bundle_info_toggle == 1 || this.bundle_info_toggle == true
        ? true
        : false;
    },
  },
};
</script>

<!-- @click="dialog = false" -->
